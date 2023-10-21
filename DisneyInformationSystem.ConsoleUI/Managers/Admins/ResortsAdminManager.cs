using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Services;

namespace DisneyInformationSystem.ConsoleUI.Managers.Admins
{
    /// <summary>
    /// Resorts Admin Manager.
    /// </summary>
    public class ResortsAdminManager : IAdminManager
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Admin Type Code of the admin that is currently signed in.
        /// </summary>
        private readonly string _adminTypeCode;

        /// <summary>
        /// Use of the <see cref="DatabaseReaderGateway"/> object.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Initializes a new instance of <see cref="ResortsAdminManager"/>.
        /// </summary>
        /// <param name="console">Console interface object.</param>
        /// <param name="adminTypeCode">Admin type code of the admin that is currently signed in.</param>
        /// <param name="databaseReaderGateway">Database reader gateway.</param>
        public ResortsAdminManager(IConsole console, string adminTypeCode, IDatabaseReaderGateway databaseReaderGateway)
        {
            _console = console;
            _adminTypeCode = adminTypeCode;
            _databaseReaderGateway = databaseReaderGateway;
        }

        /// <inheritdoc />
        public void UpdateCore()
        {
            _console.Clear();
            _console.ForegroundColor(DisColors.Cyan);
            _console.WriteLine("===== Resorts Admin Home =====");

            _console.ForegroundColor(DisColors.Yellow);
            _ = _console.TypeString("As a resort admin, you are able to add, delete, and update all information in the resort where you are an admin.\n");

            switch (_adminTypeCode)
            {
                case "DLR":
                    DirectAdminToUpdateResort("Disneyland Resort");
                    break;

                case "DPA":
                    DirectAdminToUpdateResort("Paris");
                    break;

                case "HKR":
                    DirectAdminToUpdateResort("Hong");
                    break;

                case "SDR":
                    DirectAdminToUpdateResort("Shanghai");
                    break;

                case "TDR":
                    DirectAdminToUpdateResort("Tokyo");
                    break;

                case "WDW":
                    DirectAdminToUpdateResort("Walt Disney");
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Directs admin to update information in a resort.
        /// </summary>
        /// <param name="name">Name contained in a resort name.</param>
        private void DirectAdminToUpdateResort(string name)
        {
            var resort = _databaseReaderGateway.RetrieveResortByName(name);
            var resortsService = new ResortsService(_console, resort, _databaseReaderGateway, new DatabaseWriterGateway());
            resortsService.Update();
        }
    }
}