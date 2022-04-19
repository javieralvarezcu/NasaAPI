namespace NasaApi.Models
{
    public static class DiameterCalc
    {
        public static float Calc(float diameterMin, float diameterMax)
        {
            return (diameterMin + diameterMax)/2;
        }
    }
}
