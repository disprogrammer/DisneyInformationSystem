﻿using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Deleters;
using DisneyInformationSystem.ConsoleUI.Inserters;
using DisneyInformationSystem.ConsoleUI.Services.Helpers;
using DisneyInformationSystem.ConsoleUI.Updaters;
using System.Linq;

namespace DisneyInformationSystem.ConsoleUI.Services
{
    /// <summary>
    /// Theme park service class.
    /// </summary>
    public class ThemeParkService : IResortServiceBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Use of the <see cref="DatabaseReaderGateway"/> class.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Use of the <see cref="DatabaseWriterGateway"/> class.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeParkService"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        public ThemeParkService(IConsole console, IDatabaseReaderGateway databaseReaderGateway, IDatabaseWriterGateway databaseWriterGateway)
        {
            _console = console;
            _databaseReaderGateway = databaseReaderGateway;
            _databaseWriterGateway = databaseWriterGateway;
        }

        /// <inheritdoc />
        public void Options(Resort resort)
        {
            var finished = false;
            while (!finished)
            {
                var resortsServiceHelper = new ResortsServiceHelper(_console, resort);
                var decision = resortsServiceHelper.RetrieveServiceDecision("===== Theme Park Service =====");

                switch (decision)
                {
                    case "1":
                        var themeParkInserter = new ThemeParkInserter(_console, _databaseReaderGateway, _databaseWriterGateway, resort.PIN);
                        themeParkInserter.Add();
                        break;

                    case "2":
                        UpdateThemePark(resort, resortsServiceHelper);
                        break;

                    case "3":
                        DeleteThemePark(resort, resortsServiceHelper);
                        break;

                    case "":
                        finished = true;
                        break;

                    default:
                        _console.ForegroundColor(DisColors.Red);
                        _console.WriteLine("This is not a valid option. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Deletes theme park by setting Operating to False.
        /// </summary>
        /// <param name="resort">Resort.</param>
        /// <param name="resortsServiceHelper">Resorts service helper.</param>
        private void DeleteThemePark(Resort resort, ResortsServiceHelper resortsServiceHelper)
        {
            var themeParkToDelete = RetrieveThemePark(resort);
            if (themeParkToDelete != null)
            {
                var deleter = new DeleterBase(_console, _databaseReaderGateway, _databaseWriterGateway);
                deleter.DeleteThemePark(themeParkToDelete);
            }
            else
            {
                resortsServiceHelper.NotValidMessage("theme park");
            }
        }

        /// <summary>
        /// Updates theme park properties.
        /// </summary>
        /// <param name="resort">Resort.</param>
        /// <param name="resortsServiceHelper">Resorts service helper.</param>
        private void UpdateThemePark(Resort resort, ResortsServiceHelper resortsServiceHelper)
        {
            var themeParkToUpdate = RetrieveThemePark(resort);
            if (themeParkToUpdate != null)
            {
                _console.ForegroundColor(DisColors.White);
                foreach (var propertyValuePair in RecordHelper<ThemePark>.RetrieveListOfPropertiesAndValues(themeParkToUpdate))
                {
                    _console.WriteLine(propertyValuePair);
                }

                var updater = new Updater(_console, themeParkToUpdate, _databaseWriterGateway);
                updater.Update();
            }
            else
            {
                resortsServiceHelper.NotValidMessage("theme park");
            }
        }

        /// <summary>
        /// Retrieves a theme park in a resort.
        /// </summary>
        /// <param name="resort">Resort.</param>
        /// <returns>Theme park.</returns>
        private ThemePark RetrieveThemePark(Resort resort)
        {
            var themeParks = _databaseReaderGateway.RetrieveThemeParksByResortID(resort.PIN);
            _console.ForegroundColor(DisColors.Yellow);
            _console.WriteLine("\nSelect a theme park below.");
            _console.ForegroundColor(DisColors.White);
            foreach (var themePark in themeParks)
            {
                _console.WriteLine($"- {themePark.ParkName}");
            }

            var parkDecision = _console.Prompt(">> ").ToLower();
            return themeParks.FirstOrDefault(themePark => themePark.ParkName.ToLower().Contains(parkDecision));
        }
    }
}