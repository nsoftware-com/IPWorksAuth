/*
 * IPWorks Auth 2022 .NET Edition - Sample Project
 *
 * This sample project demonstrates the usage of IPWorks Auth in a 
 * simple, straightforward way. It is not intended to be a complete 
 * application. Error handling and other checks are simplified for clarity.
 *
 * www.nsoftware.com/ipworksauth
 *
 * This code is subject to the terms and conditions specified in the 
 * corresponding product license agreement which outlines the authorized 
 * usage and restrictions.
 * 
 */

using System.Collections.Generic;
﻿using System;
using System.Threading.Tasks;
using nsoftware.async.IPWorksAuth;

class oauthDemo
{
  private static Oauth oauth = new nsoftware.async.IPWorksAuth.Oauth();

  private static void oauth_OnSSLServerAuthentication(object sender, OauthSSLServerAuthenticationEventArgs e)
  {
    if (e.Accept) return;
    Console.Write("Server provided the following certificate:\nIssuer: " + e.CertIssuer + "\nSubject: " + e.CertSubject + "\n");
    Console.Write("The following problems have been determined for this certificate: " + e.Status + "\n");
    Console.Write("Would you like to continue anyways? [y/n] ");
    if (Console.Read() == 'y') e.Accept = true;
  }

  private static void oauth_OnLaunchBrowser(object sender, OauthLaunchBrowserEventArgs e)
  {
    // Normally, the component will execute the command property to launch the browser for authorization.
    // Setting the command to an empty string will prevent a browser from opening the URL. The following 
    // line can be un-commented to exhibit this behavior.
    //e.Command = "";
    Console.WriteLine("Authorization URL: " + e.URL + "");
  }

  static async Task Main(string[] args)
  {
    if (args.Length < 5)
    {
      Console.WriteLine("usage: oauth clientID clientSecret serverAuthURL serverTokenURL authScope");
      Console.WriteLine("  clientID        the id of the client assigned when registering the application (e.g. 723966830965.apps.googleusercontent.com)");
      Console.WriteLine("  clientSecret    the secret value for the client assigned when registering the application (e.g. _bYMDLuvYkJeT_99Q-vkP1rh)");
      Console.WriteLine("  serverAuthURL   the URL of the authorization (e.g. server.https://accounts.google.com/o/oauth2/auth");
      Console.WriteLine("  serverTokenURL  the URL used to obtain the access token (e.g. https://accounts.google.com/o/oauth2/token)");
      Console.WriteLine("  authScope       the scope request or response parameter used during authorization (e.g. https://www.googleapis.com/auth/userinfo.email)");
      Console.WriteLine("\r\nExample: oauth 723966830965.apps.googleusercontent.com _bYMDLuvYkJeT_99Q-vkP1rh https://accounts.google.com/o/oauth2/auth https://accounts.google.com/o/oauth2/token https://www.googleapis.com/auth/userinfo.email");
    }
    else
    {
      oauth.OnSSLServerAuthentication += oauth_OnSSLServerAuthentication;
      oauth.OnLaunchBrowser += oauth_OnLaunchBrowser;

      try
      {
        /*This application demonstrates how to use the OAuth component to authenticate with Google using OAuth 2.0 (Device Profile). 
            It also demonstrates how to use the retrieved Authorization String with the Http and Json components to retrieve user information. 
            It will guide you through the steps to perform authorization using OAuth. 
            Please see the Introduction page within the help for more detailed instructions.

        /*Client ID and Client Secret
            Obtain and set your Client ID and Client Secret. For Google, these values can be found in the API Console:
            https://code.google.com/apis/console#access
            The values that are given as an example are from a Google test account that we have setup for you to easily run this demo. */

        oauth.ClientId = args[args.Length - 5];
        oauth.ClientSecret = args[args.Length - 4];

        /*Server Auth URL, Server Token URL, and Authorization Scope
            You can also set Server Auth URL, Server Token URL, and Authorization Scope to the values desired.
            These are preset to values for Google's User Info service.*/

        oauth.ServerAuthURL = args[args.Length - 3];
        oauth.ServerTokenURL = args[args.Length - 2];
        oauth.AuthorizationScope = args[args.Length - 1];

        // Get Authorize URL for user to authenticate

        /*The following URL will open in a web browser to authenticate to the
            service. Upon successfully authenticating and allowing access, the user will
            be redirected back to an embedded web server within the component.
            The Authorization String will then be set to the 'Authorization' property
            of the Http component and used to retrieve the user info for the authenticated
            client.*/

        string authString = await oauth.GetAuthorization();

        // Retrieve the user info for the authenticated client.
        Console.WriteLine("\nAuthorization String received:\n");
        Console.WriteLine(authString);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
  }
}


class ConsoleDemo
{
  public static Dictionary<string, string> ParseArgs(string[] args)
  {
    Dictionary<string, string> dict = new Dictionary<string, string>();

    for (int i = 0; i < args.Length; i++)
    {
      // If it starts with a "/" check the next argument.
      // If the next argument does NOT start with a "/" then this is paired, and the next argument is the value.
      // Otherwise, the next argument starts with a "/" and the current argument is a switch.

      // If it doesn't start with a "/" then it's not paired and we assume it's a standalone argument.

      if (args[i].StartsWith("/"))
      {
        // Either a paired argument or a switch.
        if (i + 1 < args.Length && !args[i + 1].StartsWith("/"))
        {
          // Paired argument.
          dict.Add(args[i].TrimStart('/'), args[i + 1]);
          // Skip the value in the next iteration.
          i++;
        }
        else
        {
          // Switch, no value.
          dict.Add(args[i].TrimStart('/'), "");
        }
      }
      else
      {
        // Standalone argument. The argument is the value, use the index as a key.
        dict.Add(i.ToString(), args[i]);
      }
    }
    return dict;
  }

  public static string Prompt(string prompt, string defaultVal)
  {
    Console.Write(prompt + (defaultVal.Length > 0 ? " [" + defaultVal + "]": "") + ": ");
    string val = Console.ReadLine();
    if (val.Length == 0) val = defaultVal;
    return val;
  }
}