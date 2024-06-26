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

program ldap;

uses
  Forms,
  ldapf in 'ldapf.pas' {FormLdap};

begin
  Application.Initialize;

  Application.CreateForm(TFormLdap, FormLdap);
  Application.Run;
end.


         
