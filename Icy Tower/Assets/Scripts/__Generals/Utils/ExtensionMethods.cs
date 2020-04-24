public static class ExtensionMethods {

    public static float Map(this float value, float fromSource, float toSource, float fromTarget, float toTarget) {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public static bool AmIAbleToBuyIt(int myCurrencyAmount, int price) {
        if (myCurrencyAmount - price < 0) {
            return false;
        } else {
            return true;
        }
    }

}

