﻿using Microsoft.Extensions.Options;

using MediatR;

using WeatherForecast.Passport.Api.Requests.Handlers;
using WeatherForecast.Api.Requests;
using WeatherForecast.Api.Responses;
using WeatherForecast.Api.Options;
using WeatherForecast.Api.Services;
using WeatherForecast.Domain.Aggregates.Account;
using WeatherForecast.Domain.Aggregates.Account.Enums;

namespace WeatherForecast.Passport.Api.Requests.AccountRequests.Handlers;

public class ForgotPasswordRequestHandler : IRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>
{
    private readonly JWTOptions _jwtOptions;
    private readonly IEmailService _emailService;
    private readonly IAccountRepo _accountRepo;
    private readonly ILogger<SignInEmailRequestHandler> _logger;

    public ForgotPasswordRequestHandler(
        IAccountRepo accountRepo,
        IEmailService emailService,
        IOptions<JWTOptions> jwtOptions,
        ILogger<SignInEmailRequestHandler> logger)
    {
        _accountRepo = accountRepo;
        _emailService = emailService;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }

    public async Task<ForgotPasswordResponse> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        try
        {      
            var account = await _accountRepo.FindByEmailAsync(request.Email);

            if (account is null) throw new BadHttpRequestException("Account not found");

            account.ActivationCode = $"{new Random().Next(000000, 999999)}";
            account.AccessStatus = AccessStatus.WaitChangePassword;

            var isSendToEmail = await _emailService.SendEmailAsync(
                  account.Nickname,
                  account.Email,
                  "Recovery access in Library", 
                  $"Activation link: {account.ActivationCode}");

            if(!isSendToEmail) throw new BadHttpRequestException("Not send code send to email");

            await _accountRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new ForgotPasswordResponse(Message: "Code send to email");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with signin. Error: {ex}");

            throw;
        }
    }
}
