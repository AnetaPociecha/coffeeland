using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Queries.Handlers;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System;


namespace Coffeeland.Messaging.Commands.Handlers
{
    public class UpdatePersonalDataCommandHandler : ICommandHandler<UpdatePersonalDataCommand>
    {
        public IResult Handle(UpdatePersonalDataCommand command)
        {
            int clientId = SessionRepository.GetClientIdOfSession(command.sessionToken);
            if (clientId == -1)
                throw new Exception();

            if (!InputChecker.isValidEmail(command.email) ||
                !InputChecker.isValidName(command.firstName) ||
                !InputChecker.isValidName(command.lastName) ||
                (command.receiveNewsletterEmail &&
                !InputChecker.isValidEmail(command.newsletterEmail)))
            {
                throw new Exception();
            }

            DatabaseQueryProcessor.UpdateClientCredentials(clientId, "email", command.email);
            DatabaseQueryProcessor.UpdateClientCredentials(clientId, "firstName", command.firstName);
            DatabaseQueryProcessor.UpdateClientCredentials(clientId, "lastName", command.lastName);
            if (command.changePassword)
                DatabaseQueryProcessor.UpdateClientCredentials(clientId, "password", PasswordEncryptor.encryptSha256(command.newPassword));
            if (command.receiveNewsletterEmail)
                DatabaseQueryProcessor.UpdateClientCredentials(clientId, "newsletterEmail", command.newsletterEmail);

            return new GetPersonalDataQueryHandler().Handle(new GetPersonalDataQuery() {
                sessionToken = command.sessionToken
            });
        }
    }
}