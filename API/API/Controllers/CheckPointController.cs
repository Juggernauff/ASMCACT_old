using API.Context;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Ports;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPointController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly ApplicationContext _applicationContext;

        private readonly SerialPort _serialPort;

        public CheckPointController(ILogger<IdentityController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;

            _serialPort = new SerialPort();
        }

        [HttpPost("Open")]
        public async Task<IActionResult> Open(CheckPoint checkPoint)
        {
            if (checkPoint == null) return BadRequest(checkPoint);

            CheckPoint? currentCheckPoint = await _applicationContext.CheckPoints
                .FirstOrDefaultAsync(cp => cp.CompanyId == checkPoint.CompanyId && cp.Name == checkPoint.Name);

            if (currentCheckPoint == null) return BadRequest(currentCheckPoint);

            try
            {
                if (_serialPort.IsOpen) _serialPort.Close();

                ConfiguringSerialPort(_serialPort, "COM1", 9600);

                _serialPort.Open();
                SendCommand(_serialPort, "open");
                _serialPort.Close();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        private void SendCommand(SerialPort serialPort, string command)
        {
            serialPort.Write(command);
        }

        private void ConfiguringSerialPort(SerialPort serialPort, string portName, int baudRate)
        {
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
        }
    }
}
