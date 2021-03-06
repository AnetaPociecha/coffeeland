﻿using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System;
using System.Threading;
using Coffeeland.MailService;

namespace Coffeeland.Messaging.Commands.Handlers
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        public IResult Handle(SignInCommand command)
        {
            var foundClient = DatabaseQueryProcessor.GetClient(command.email, PasswordEncryptor.encryptSha256(command.password));

            if (foundClient == null)
            {
                return new SignInInfoDto()
                {
                    isSuccess = false,
                    sessionToken = null
                };
            }

            var sessionToken = SessionRepository.StartNewSession(foundClient.clientId);
            return new SignInInfoDto()
            {
                isSuccess = true,
                sessionToken = sessionToken
            };

        }
    }
}