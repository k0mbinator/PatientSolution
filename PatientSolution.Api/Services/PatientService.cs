
namespace PatientSolution.Api.Services;

public class PatientService : IPatientService
{
    private readonly string _connectionString;

    public PatientService()
    {
        string? dbPassword = Environment.GetEnvironmentVariable("PATIENT_APP_DB_PASSWORD");
        if (string.IsNullOrEmpty(dbPassword))
        {
            throw new InvalidOperationException("Datenbank-Passwort in Umgebungsvariable PATIENT_APP_DB_PASSWORD ist nicht gesetzt.\n"
            + "Bitte setze die Umgebungsvariable, um auf die Datenbank zugreifen zu können.");

        }
        _connectionString = $"DATA SOURCE=localhost:1521/XEPDB1;USER ID=PatientAppUser;PASSWORD={dbPassword};";
    }





    public async Task<List<Patient>> GetPatientsAsync()
    {
        var patients = new List<Patient>();
        string selectQuery = "SELECT * FROM PATIENTS ORDER BY PATIENT_ID"; // Stelle sicher, dass der Tabellenname GROSS ist!

        await using (var connection = new OracleConnection(_connectionString))
        {
            try
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand(selectQuery, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        patients.Add(MapPatient(reader));
                    }
                }
            }
            catch (OracleException ex)
            {
                // Fehler auf der Konsole (im Fall des Blazor-Servers) ausgeben und Exception weiterwerfen
                await Console.Error.WriteLineAsync($"Oracle Fehler beim Laden der Patienten: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync($"Allgemeiner Fehler beim Laden der Patienten: {ex.Message}");
                throw;
            }
        }
        return patients;
    }

    private static Patient MapPatient(OracleDataReader reader)
    {
        
        return new Patient
        {
            PatientId = reader["PATIENT_ID"].ToString() ?? string.Empty, // PatientId ist required string
            FirstName = reader["FIRST_NAME"].ToString() ?? string.Empty, // FirstName ist required string
            LastName = reader["LAST_NAME"].ToString() ?? string.Empty,   // LastName ist required string

            DateOfBirth = reader["DATE_OF_BIRTH"] is DBNull ? null : (DateTime?)reader["DATE_OF_BIRTH"], // Nullable DateTime
            Gender = reader["GENDER"] is DBNull ? null : reader["GENDER"].ToString(), // Nullable string
            AddressStreet = reader["ADDRESS_STREET"] is DBNull ? null : reader["ADDRESS_STREET"].ToString(), // Nullable string
            AddressCity = reader["ADDRESS_CITY"] is DBNull ? null : reader["ADDRESS_CITY"].ToString(),     // Nullable string
            AddressState = reader["ADDRESS_STATE"] is DBNull ? null : reader["ADDRESS_STATE"].ToString(),   // Nullable string
            AddressZip = reader["ADDRESS_ZIP"] is DBNull ? null : reader["ADDRESS_ZIP"].ToString(),       // Nullable string
            Hl7MessageType = reader["HL7_MESSAGE_TYPE"] is DBNull ? null : reader["HL7_MESSAGE_TYPE"].ToString(), // Nullable string

            ReceivedDate = reader["RECEIVED_DATE"] is DBNull ? DateTime.MinValue : (DateTime)reader["RECEIVED_DATE"] // ReceivedDate ist required DateTime, muss immer einen Wert haben.
                                                                                                                     // Annahme: ReceivedDate ist in DB niemals NULL, ansonsten DBNull prüfen und einen sinnvollen Standardwert setzen
        };
    }
}