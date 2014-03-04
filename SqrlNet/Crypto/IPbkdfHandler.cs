namespace SqrlNet.Crypto
{
	/// <summary>
	/// Provides all the PBKDF functionality needed by the SQRL process.
	/// </summary>
	public interface IPbkdfHandler
	{
		#region Events

		/// <summary>
		/// Gets or sets the iteration complete event.
		/// </summary>
		/// <value>
		/// The iteration complete event.
		/// </value>
		/// <remarks>
		/// This event will fire whenever the GeneratePasswordKey method finishes
		/// with a single iteration of the SCRYPT algorithm.  Keep in mind that the
		/// GeneratePasswordKey method is called during the VerifyPassword method,
		/// which will cause the delegate to fire during VerifyPassword as well.
		/// </remarks>
		event IterationCompleteHandler OnIterationComplete;

		#endregion

		#region Methods

		/// <summary>
		/// Generates the password key.
		/// </summary>
		/// <returns>
		/// The password key.
		/// </returns>
		/// <param name='password'>
		/// The password.
		/// </param>
		/// <param name='salt'>
		/// The salt.
		/// </param>
		/// <param name='iterations'>
		/// The number of iterations.
		/// </param>
		byte[] GeneratePasswordKey(string password, byte[] salt, int iterations = 1);

		/// <summary>
		/// Verifies the password.
		/// </summary>
		/// <returns>
		/// Returns true if the password given matches the partial hash given
		/// </returns>
		/// <param name='password'>
		/// The password to be verified.
		/// </param>
		/// <param name='salt'>
		/// The salt added to the password.
		/// </param>
		/// <param name='partialHash'>
		/// The lower 128 bits of a hash of the output of the PBKDF (GeneratePasswordKey).
		/// </param>
		/// <param name='iterations'>
		/// The number of iterations to run through.
		/// </param>
		bool VerifyPassword(string password, byte[] salt, byte[] partialHash, int iterations = 1);

		/// <summary>
		/// Gets the partial hash used for password verification from the password key generated from the PBKDF.
		/// </summary>
		/// <returns>
		/// The partial hash from password key.
		/// </returns>
		/// <param name='passwordKey'>
		/// Password key.
		/// </param>
		byte[] GetPartialHashFromPasswordKey(byte[] passwordKey);

		#endregion
	}

	/// <summary>
	/// A delegate to be called upon the completion of an iteration
	/// </summary>
	/// <param name='iteration'>
	/// The recently finished iteration.
	/// </param>
	/// <remarks>
	/// This delegate should handle any per-iteration updates or calculations
	/// that need to occurr while the PBKDF handler is iterating over the
	/// SCRYPT hashing algorithm.
	/// </remarks>
	public delegate void IterationCompleteHandler(int iteration);
}