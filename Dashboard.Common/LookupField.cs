using System;

namespace Dashboard.Common
{
	[Serializable]
	public class LookupField : IEquatable<LookupField>
	{
		#region Constructors

		public LookupField(string value, string text)
		{
			Value = value;
			Text = text;
		}

		public LookupField(string value, string text, string parentValue)
		{
			Value = value;
			Text = text;
			ParentValue = parentValue;
		}

		#endregion

		#region Properties

		public string Value { get; private set; }
		public string Text { get; private set; }
		public string ParentValue { get; private set; }

		#endregion

		#region IEquatable Implementation

		public bool Equals(LookupField other)
		{
			bool result = false;

			if (!Object.ReferenceEquals(other, null))
			{
				if (Object.ReferenceEquals(this, other))
				{
					result = true;
				}
				else
				{
					result = ((Value ?? String.Empty).Equals(other.Value ?? String.Empty) &&
							  (Text ?? String.Empty).Equals(other.Text ?? String.Empty) &&
							  (ParentValue ?? String.Empty).Equals(other.ParentValue ?? String.Empty));	
				}
			}

			return result;
		}

		#endregion

		#region Overrides - System.Object

		public override int GetHashCode()
		{
			int hashLookupValue = Value == null ? 0 : Value.GetHashCode();
			int hashLookupText = Text == null ? 0 : Text.GetHashCode();
			int hashLookupParent = ParentValue == null ? 0 : ParentValue.GetHashCode();

			return hashLookupValue ^ hashLookupText ^ hashLookupParent;
		}

		#endregion
	}
}
