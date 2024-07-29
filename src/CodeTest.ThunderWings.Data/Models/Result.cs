namespace CodeTest.ThunderWings.Data.Models
{
	public class Result<T>
	{
		public bool IsSuccess { get; private set; }
		public string? Message { get; private set; }
		public T? Value { get; private set; }

		public static Result<T> Failure(string message)
		{
			return new Result<T>()
			{
				IsSuccess = false,
				Message = message.Trim(),
			};
		}

		public static Result<T> Success(string message)
		{
			return new Result<T>()
			{
				IsSuccess = true,
				Message = message.Trim(),
			};
		}

		public static Result<T> Success(T value)
		{
			return new Result<T>()
			{
				IsSuccess = true,
				Value = value,
			};
		}
	}
}