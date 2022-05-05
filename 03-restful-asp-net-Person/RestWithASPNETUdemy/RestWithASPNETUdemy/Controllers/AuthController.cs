﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Controllers;

[ApiVersion("1")]
[Route("api/[controller]/v{version:apiVersion}")]
[ApiController]
public class AuthController : ControllerBase
{
    private ILoginBusiness _loginBusiness;

    public AuthController(ILoginBusiness loginBusiness)
    {
        _loginBusiness = loginBusiness;
    }

    [HttpPost]
    [Route("signin")]
    public IActionResult Signin([FromBody] UserVO user)
    {
        if (user == null) return BadRequest("Invalid client request");

        var token = _loginBusiness.ValidateCredentials(user);

        if (token == null) return Unauthorized();
        
        return Ok(token);
    }

    [HttpPost]
    [Route("refresh")]
    public IActionResult Refresh([FromBody] TokenVO tokenVo)
    {
        if (tokenVo == null) return BadRequest("Invalid client request");

        var token = _loginBusiness.ValidateCredentials(tokenVo);

        if (token == null) return BadRequest("Invalid client request");

        return Ok(token);
    }
}
