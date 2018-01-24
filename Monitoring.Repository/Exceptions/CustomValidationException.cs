using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Repository.Exceptions
{
    /// <summary>
    /// Custom exception for CRUD validation
    /// </summary>
    public class CustomValidationException : Exception
    {
        #region Attributes

        ICollection<string> _messages;

        #endregion

        public CustomValidationException()
        {
            _messages = new List<string>();
        }

        #region Properties

        /// <summary>
        /// Validation messages
        /// </summary>
        public IEnumerable<string> Messages { get { return _messages; } set { _messages = (ICollection<string>)value; } }

        #endregion

        #region Methods

        /// <summary>
        /// Add a valition message to the validation erros list.
        /// </summary>
        /// <param name="message">Message to add</param>
        /// <param name="throwImmediately">If true, throws CustomValidationException after adding the message.</param>
        public void AddMessage(string message, bool throwImmediately = true)
        {
            _messages.Add(message);
            if (throwImmediately)
                throw this;
        }

        /// <summary>
        /// Throws CustomValidationException if has messages.
        /// </summary>
        public void ThrowIfHasErrors()
        {
            if (Messages != null && (Messages as List<string>).Count > 0)
                throw this;
        }

        #endregion
    }
}
