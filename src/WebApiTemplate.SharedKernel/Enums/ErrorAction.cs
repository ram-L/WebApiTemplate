namespace WebApiTemplate.SharedKernel.Enums
{
    public enum ErrorHandlingOption
    {
        /// <summary>
        /// No action will be taken when an error is encountered.
        /// </summary>
        None,

        /// <summary>
        /// An exception will be thrown when an error is encountered.
        /// </summary>
        Throw,

        /// <summary>
        /// The default value of the object or collection will be returned when an error is encountered.
        /// </summary>
        ReturnDefault,

        /// <summary>
        /// A new instance of the object or collection will be returned when an error is encountered.
        /// </summary>
        ReturnEmpty
    }
}
