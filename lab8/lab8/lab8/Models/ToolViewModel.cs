using System;

namespace lab8.Models
{
	public class ToolViewModel
	{
        public static (float?, float?) QuadraticFormula(float a, float b, float c)
        {

            if (a == 0 && b != 0) return (-c / b == 0 ? 0 : -c / b, null);

            if (a == 0 && b == 0) return c == 0 ? (float.PositiveInfinity, null) : (null, null);

            float delta = (b * b) - (4 * a * c);
            float sqrt_delta = (float)Math.Sqrt(delta);

            float? root1 = null;
            float? root2 = null;

            if (delta >= 0) root1 = ((-b) + sqrt_delta) / (2 * a);

            if (delta > 0) root2 = ((-b) - sqrt_delta) / (2 * a);

            return (root1 == 0 ? 0 : root1, root2 == 0 ? 0 : root2);
        }
    }
}

