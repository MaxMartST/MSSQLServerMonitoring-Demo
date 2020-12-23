using NUnit.Framework;

namespace MSSQLServerMonitoring.Application.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_Success_Tests()
        {
            // Arrange
            var calculator = new Calculator.Calculator();

            // Act
            var result = calculator.Add( 1, 2 );

            // Assert
            Assert.That( result, Is.EqualTo( 3 ) );
        }
    }
}
