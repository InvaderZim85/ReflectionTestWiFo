namespace WiFoTestApplication.DataObjects
{
    public sealed class ControlMetadata
    {
        /// <summary>
        /// Gets or sets the name of the control
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value which indicates if the control is required
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the original value of the control
        /// </summary>
        public object OriginalValue { get; set; }
    }
}
