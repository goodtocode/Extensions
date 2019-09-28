
namespace GoodToCode.Extensions.Mathematics
{
    /// <summary>
    /// MathHelper - Read Only Collection
    /// </summary>
    
    public class AspectRatio
    {
        /// <summary>
        /// Gets new width for changing height
        /// </summary>
        /// <param name="original">Original square</param>
        /// <param name="newHeight">New height</param>
        /// <returns>Width given original item was resized</returns>
        public static Square WidthChange(Square original, int newHeight)
        {
            var returnValue = new Square();

            // Height is only specified, have to calculate width
            decimal multiplier = Arithmetic.Divide(newHeight.ToDecimal(), original.Height.ToDecimal());
            // Resize
            returnValue.Width = Arithmetic.Multiply(original.Width.ToDecimal(), multiplier).ToInt();

            return returnValue;
        }

        /// <summary>
        /// Gets new width for changing height
        /// </summary>
        /// <param name="original">Original square</param>
        /// <param name="newWidth">New width</param>
        /// <returns>Width given original item was resized</returns>
        public static Square HeightChange(Square original, int newWidth)
        {
            var returnValue = new Square();

            // Height is only specified, have to calculate width
            decimal multiplier = Arithmetic.Divide(newWidth.ToDecimal(), original.Width.ToDecimal());
            // Resize
            returnValue.Height = Arithmetic.Multiply(original.Height.ToDecimal(), multiplier).ToInt();

            return returnValue;
        }
    }
}
