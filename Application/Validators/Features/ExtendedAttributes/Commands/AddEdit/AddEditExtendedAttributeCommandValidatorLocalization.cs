﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Features.ExtendedAttributes.Commands.AddEdit
{
    public class AddEditExtendedAttributeCommandValidatorLocalization
    {
        // for localization
    }

    public abstract class AddEditExtendedAttributeCommandValidator<TId, TEntityId, TEntity, TExtendedAttribute> : AbstractValidator<AddEditExtendedAttributeCommand<TId, TEntityId, TEntity, TExtendedAttribute>>
        where TEntity : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>, IEntity<TEntityId>
        where TExtendedAttribute : AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>, IEntity<TId>
        where TId : IEquatable<TId>
        where TEntityId : IEquatable<TEntityId>
    {
        protected AddEditExtendedAttributeCommandValidator()
        {
            RuleFor(request => request.EntityId)
                .NotEqual(default(TEntityId)).WithMessage(x => "Entity is required!");
            RuleFor(request => request.Key)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "Key is required!");

            When(request => request.Type == EntityExtendedAttributeType.Decimal, () =>
            {
                RuleFor(request => request.Decimal).NotNull().WithMessage(x => string.Format("Decimal value is required using {0} type!", x.Type.ToString()));
            });

            When(request => request.Type == EntityExtendedAttributeType.Text, () =>
            {
                RuleFor(request => request.Text).NotNull().WithMessage(x => string.Format("Text value is required using {0} type!", x.Type.ToString()));
            });

            When(request => request.Type == EntityExtendedAttributeType.DateTime, () =>
            {
                RuleFor(request => request.DateTime).NotNull().WithMessage(x => string.Format("DateTime value is required using {0} type!", x.Type.ToString()));
            });

            When(request => request.Type == EntityExtendedAttributeType.Json, () =>
            {
                //RuleFor(request => request.Json).MustBeJson(new JsonValidator<AddEditExtendedAttributeCommand<TId, TEntityId, TEntity, TExtendedAttribute>>(jsonSerializer))
                //    .WithMessage(x => string.Format(localizer["Json value must be a valid json string using {0} type!"], x.Type.ToString()));
                RuleFor(request => request.Json).NotNull().WithMessage(x => string.Format("Json value is required using {0} type!", x.Type.ToString()));
            });
        }
    }
}
