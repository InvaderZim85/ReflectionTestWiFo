using System;

namespace WiFoTestApplication.DataObjects
{
    /// <summary>
    /// Represents a person
    /// </summary>
    internal sealed class Person
    {
        /// <summary>
        /// Gets or sets the id of the person
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the person
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birthday of the person
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the account number
        /// </summary>
        public int AccountNumber { get; set; }
    }
}
