using System.Net;
using UnityEngine;

public class Internet 
{
    // Check Internet Connection
    public static bool ConnectionTest()
    {
        try
        {
            using (var client = new WebClient())

            using (var stream = client.OpenRead("http://www.google.com"))
            {
                Debug.Log("Connection Succeed.");

                return true;
            }
        }
        catch
        {
            Debug.LogError("Connection Failed.");

            return false;
        }
    }

}
