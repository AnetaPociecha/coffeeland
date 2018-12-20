using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;
using System;

namespace Coffeeland.Messaging.Commands.Handlers
{
    public class RegisterNewClientCommandHandler : ICommandHandler<RegisterNewClientCommand>
    {
        public IResult Handle(RegisterNewClientCommand command)
        {
            if (!InputChecker.isValidEmail(command.email) ||
                !InputChecker.isValidName(command.firstName) ||
                !InputChecker.isValidName(command.lastName) ||
                (command.receiveNewsletterEmail && 
                !InputChecker.isValidEmail(command.newsletterEmail)))
            {
                throw new Exception();
            }

            DatabaseQueryProcessor.CreateNewClient(
                command.email,
                command.firstName,
                command.lastName,
                PasswordEncryptor.encryptSha256(command.password),
                command.receiveNewsletterEmail ? command.newsletterEmail : ""
                );

            return new SuccessInfoDto()
            {
                isSuccess = true
            };
        }
    }
}