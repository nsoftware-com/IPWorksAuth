(*
 * IPWorks Auth 2024 Delphi Edition - Sample Project
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
 *)
unit oauthf;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics,
  Controls, Forms, Dialogs, Grids, ValEdit, StdCtrls,
  ExtCtrls, ComCtrls, jpeg, ipaoauth, ipacore;

type
  TFormOauth = class(TForm)
    Label1: TLabel;
    ipaOAuth1: TipaOAuth;
    Panel1: TPanel;
    txtClientID: TEdit;
    txtClientSecret: TEdit;
    Label2: TLabel;
    Label3: TLabel;
    Panel2: TPanel;
    txtServerAuthURL: TEdit;
    txtServerTokenURL: TEdit;
    txtAuthScope: TEdit;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    btnAuthorize: TButton;
    Panel3: TPanel;
    txtAuthString: TEdit;
    Label7: TLabel;
    Panel5: TPanel;
    Label12: TLabel;
    Label13: TLabel;
    Label14: TLabel;
    Label15: TLabel;
    Label16: TLabel;
    procedure btnAuthorizeClick(Sender: TObject);
    procedure ipaOAuth1LaunchBrowser(Sender: TObject; const URL: string;
      var Command: string);
    procedure ipaOAuth1ReturnURL(Sender: TObject; const URLPath,
      QueryString: string; var ResponseHeaders, ResponseBody: string);
  private

  public
    { Public declarations }
  end;

var
  FormOauth: TFormOauth;

implementation

{$R *.dfm}

procedure TFormOauth.btnAuthorizeClick(Sender: TObject);
begin
  try
    begin
      ipaOauth1 := TipaOauth.Create(nil);
      ipaOauth1.Reset;
      ipaOauth1.Config('BrowserResponseTimeout=30');
      ipaOAuth1.ClientId := txtClientID.Text;
      ipaOAuth1.ClientSecret := txtClientSecret.Text;
      ipaOAuth1.ServerAuthURL := txtServerAuthURL.Text;
      ipaOAuth1.ServerTokenURL := txtServerTokenURL.Text;
      ipaOAuth1.AuthorizationScope := txtAuthScope.Text;
      btnAuthorize.Enabled := false;
      txtAuthString.Text := ipaOAuth1.GetAuthorization();
    end;
  Except on E: Exception do
    ShowMessage('Error: ' + E.Message);
  end;
  btnAuthorize.Enabled := true;
end;

procedure TFormOauth.ipaOAuth1LaunchBrowser(Sender: TObject; const URL: string;
  var Command: string);
begin
  //This event is fired before the launching of a browser
  // (i.e. when GetAuthorization is called).

  //The Command parameter provides you with command that will be executed by the
  // component. The URL parameter provides you with the authorization URL which
  // the user will be directed to authenticate. Both the Command and URL
  // parameters can be overridden in this event.
end;

procedure TFormOauth.ipaOAuth1ReturnURL(Sender: TObject; const URLPath,
  QueryString: string; var ResponseHeaders, ResponseBody: string);
begin
  //This event is fired when the user is redirected to the embedded web server
  // (i.e. after successfully authenticating and allowing access).

  //You can set the ResponseHeaders and ResponseBody parameters to provide a
  // custom response that the user will see in their browser.
end;

end.

