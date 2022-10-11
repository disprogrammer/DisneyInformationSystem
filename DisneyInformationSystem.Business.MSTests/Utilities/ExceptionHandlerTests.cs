using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Utilities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExceptionHandlerTests
    {
        [TestMethod, TestCategory("Business Test"), ExpectedException(typeof(AddressSignNotFoundException))]
        public void RegistrationHelper_CheckIfEmailContainsAddressSign_WhenProvidedEmailDoesNotContainAddressSign_ShouldThrowAddressSignNotFoundException()
        {
            // Arrange
            var emailAddress = "MagicKingdom1971gmail.com";

            // Act
            ExceptionHandler.CheckIfEmailContainsAddressSign(emailAddress);

            // Assert
        }

        [TestMethod, TestCategory("Business Test")]
        public void RegistrationHelper_CheckIfEmailContainsAddressSign_WhenProvidedEmailDoesContainAddressSign_ShouldNotThrowAddressSignNotFoundException()
        {
            // Arrange
            var emailAddress = "MagicKingdom1971@gmail.com";

            try
            {
                // Act
                ExceptionHandler.CheckIfEmailContainsAddressSign(emailAddress);
            }
            catch (AddressSignNotFoundException exception)
            {
                // Assert
                Assert.Fail(BusinessTestHelper.FailMessage(exception.GetType().Name));
            }
        }

        [TestMethod, TestCategory("Business Test"), ExpectedException(typeof(EmailAlreadyExistsException))]
        public void RegistrationHelper_CheckThatEmailDoesNotAlreadyExist_WhenEmailAddressAlreadyExists_ShouldThrowEmailAlreadyExistsException()
        {
            // Arrange
            var listOfEmailAddresses = new List<Admin>
            {
                new Admin("A1234567890", "TAD", "James", "McKinney", "epcot@center.com", "DisneySprings", 100)
            }.Select(admin => admin.EmailAddress).ToList();

            // Act
            ExceptionHandler.CheckThatEmailDoesNotAlreadyExist("epcot@center.com", listOfEmailAddresses);

            // Assert
        }

        [TestMethod, TestCategory("Business Test")]
        public void RegistrationHelper_CheckThatEmailDoesNotAlreadyExist_WhenEmailAddressDoesNotAlreadyExist_ShouldNotThrowEmailAlreadyEExistsException()
        {
            // Arrange
            var listOfEmailAddresses = new List<Admin>
            {
                new Admin("A1234567890", "TAD", "James", "McKinney", "epcot@center.com", "DisneySprings", 100)
            }.Select(admin => admin.EmailAddress).ToList();

            try
            {
                // Act
                ExceptionHandler.CheckThatEmailDoesNotAlreadyExist("blizzard@beach.com", listOfEmailAddresses);
            }
            catch (EmailAlreadyExistsException exception)
            {
                // Assert
                Assert.Fail(BusinessTestHelper.FailMessage(exception.GetType().Name));
            }
        }

        [TestMethod, TestCategory("Business Test"), ExpectedException(typeof(AdminTypeInvalidException))]
        [DataRow("ASMR")]
        [DataRow("MK")]
        [DataRow("TA2")]
        [DataRow("ATM")]
        public void RegistrationHelper_CheckIfAdminTypeCodeIsValid_WhenAdminTypesAreNotValid_ShouldThrowAdminTypeInvalidException(string adminTypeCode)
        {
            // Arrange
            var listOfAdminTypeCodes = new List<AdminTypes> { new AdminTypes("TAD", "Top Admin") };

            // Act
            ExceptionHandler.CheckIfAdminTypeCodeIsValid(adminTypeCode, listOfAdminTypeCodes);

            // Assert
        }

        [TestMethod, TestCategory("Business Test")]
        public void RegistrationHelper_CheckIfAdminTypeCodeIsValid_WhenAdminTypeIsValid_ShouldNotThrowAdminTypeInvalidException()
        {
            // Arrange
            var listOfAdminTypeCodes = new List<AdminTypes> { new AdminTypes("TAD", "Top Admin") };

            try
            {
                // Act
                ExceptionHandler.CheckIfAdminTypeCodeIsValid("TAD", listOfAdminTypeCodes);
            }
            catch (AdminTypeInvalidException exception)
            {
                // Assert
                Assert.Fail(BusinessTestHelper.FailMessage(exception.GetType().Name));
            }
        }

        [TestMethod, TestCategory("Business Test"), ExpectedException(typeof(PhoneNumberInvalidException))]
        [DataRow("123456j890")]
        [DataRow("123456789")]
        public void RegistrationHelper_CheckIfPhoneNumberIsValid_WhenPhoneNumberIsInvalid_ShouldThrowPhoneNumberInvalidException(string phoneNumber)
        {
            // Arrange

            // Act
            ExceptionHandler.CheckIfPhoneNumberIsValid(phoneNumber);

            // Assert
        }

        [TestMethod, TestCategory("Business Test")]
        public void RegistrationHelper_CheckIfPhoneNumberIsValid_WhenPhoneNumberIsValid_ShouldNotThrowPhoneNumberInvalidException()
        {
            // Arrange

            try
            {
                // Act
                ExceptionHandler.CheckIfPhoneNumberIsValid("1234567890");
            }
            catch (PhoneNumberInvalidException exception)
            {
                // Assert
                Assert.Fail(BusinessTestHelper.FailMessage(exception.GetType().Name));
            }
        }

        [TestMethod, TestCategory("Business Test"), ExpectedException(typeof(AcronymInvalidException))]
        [DataRow("WD1")]
        [DataRow("WD")]
        [DataRow("WDWR")]
        public void RegistrationHelper_CheckIfResortAcronymIsValid_WhenAcronymIsNotValid_ShouldThrowResortAcronymInvalidException(string acronym)
        {
            // Arrange

            // Act
            ExceptionHandler.CheckIfAcronymIsValid(acronym);

            // Assert
        }

        [TestMethod, TestCategory("Business Test")]
        public void RegistrationHelper_CheckIfResortAcronymIsValid_WhenAcronymIsValid_ShouldNotThrowResortAcronymInvalidException()
        {
            // Arrange

            try
            {
                // Act
                ExceptionHandler.CheckIfAcronymIsValid("WDW");
            }
            catch (AcronymInvalidException exception)
            {
                // Assert
                Assert.Fail(BusinessTestHelper.FailMessage(exception.GetType().Name));
            }
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ExceptionHandler_CheckIfInputIsNumber_WhenInputIsEmpty_ShouldReturnZero()
        {
            // Arrange

            // Act
            var returnValue = ExceptionHandler.CheckIfInputIsNumber("");

            // Assert
            Assert.IsTrue(returnValue == 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("3E")]
        [DataRow("!4")]
        [ExpectedException(typeof(FormatException))]
        public void ExceptionHandler_CheckIfInputIsNumber_WhenInputsAreInvalid_ShouldThrowFormatException(string input)
        {
            // Arrange

            // Act
            _ = ExceptionHandler.CheckIfInputIsNumber(input);

            // Assert
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ExceptionHandler_CheckIfInputIsNumber_WhenInputIsNumber_ShouldReturnNumber()
        {
            // Arrange

            // Act
            var returnValue = ExceptionHandler.CheckIfInputIsNumber("4");

            // Assert
            Assert.IsTrue(returnValue == 4, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [ExpectedException(typeof(FormatException))]
        public void ExceptionHandler_CheckDateTime_WhenInputIsNotDateTimeType_ShouldThrowFormatException()
        {
            // Arrange
            var dateTimeString = "abc";

            // Act
            _ = ExceptionHandler.CheckDateTime(dateTimeString);

            // Assert
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ExceptionHandler_CheckDateTime_WhenInputIsDateTimeType_ShouldReturnDateTime()
        {
            // Arrange
            var expectedDateTime = new DateTime(1955, 07, 17);
            var userInputDate = "1955-07-17";

            // Act
            var actualDateTime = ExceptionHandler.CheckDateTime(userInputDate);

            Assert.AreEqual(expectedDateTime, actualDateTime, AssertMessage.ExpectValuesToBeEqual);
        }
    }
}