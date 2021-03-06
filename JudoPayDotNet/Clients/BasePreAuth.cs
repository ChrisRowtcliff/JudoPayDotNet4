﻿using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class BasePreAuth : JudoPayClient
    {
        protected readonly IValidator<CardPaymentModel> CardPaymentValidator = new CardPaymentValidator();
        protected readonly IValidator<TokenPaymentModel> TokenPaymentValidator = new TokenPaymentValidator();
        private readonly string _createPreAuthAddress;

        protected BasePreAuth(ILog logger, IClient client, string createPreAuthAddress) : base(logger, client)
        {
            _createPreAuthAddress = createPreAuthAddress;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPreAuth)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(CardPaymentValidator, cardPreAuth);

            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(_createPreAuthAddress, cardPreAuth);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPreAuth)
        {
            var validationError = Validate<TokenPaymentModel, ITransactionResult>(TokenPaymentValidator, tokenPreAuth);

            return validationError ?? PostInternal<TokenPaymentModel, ITransactionResult>(_createPreAuthAddress, tokenPreAuth);
        }
    }
}
