using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConverToDecimal(firstNumber) + ConverToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("multiply/{firstNumber}/{secondNumber}")]
        public IActionResult Multiply(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var multiply = ConverToDecimal(firstNumber) * ConverToDecimal(secondNumber);
                return Ok(multiply.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var subtraction = ConverToDecimal(firstNumber) - ConverToDecimal(secondNumber);
                return Ok(subtraction.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var divison = ConverToDecimal(firstNumber) / ConverToDecimal(secondNumber);
                return Ok(divison.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var mean = (ConverToDecimal(firstNumber) + ConverToDecimal(secondNumber)) / 2;
                return Ok(mean.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("square-root/{firstNumber}")]
        public IActionResult SquareRoot(string firstNumber)
        {
            if (IsNumeric(firstNumber))
            {
                var squareRoot = Math.Sqrt((double)ConverToDecimal(firstNumber));
                return Ok(squareRoot.ToString());
            }

            return BadRequest("Invalid Input");
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number);
            return isNumber;

        }

        private decimal ConverToDecimal(string strNumber)
        {
            decimal decimalValue;

            if(decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }

            return 0;
        }
    }
}   