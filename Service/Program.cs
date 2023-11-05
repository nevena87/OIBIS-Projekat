﻿using CertificateManager;
using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak26
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            //string name = "wcfservice";
            Console.WriteLine(name);

            NetTcpBinding bindingClient = new NetTcpBinding();
            string addressClient = "net.tcp://localhost:4001/DataBaseService";

            //Windows Authentification
            bindingClient.Security.Mode = SecurityMode.Transport;
            bindingClient.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            bindingClient.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            ServiceHost hostClient = new ServiceHost(typeof(DataBaseService));
            hostClient.AddServiceEndpoint(typeof(IDataBaseManagement), bindingClient, addressClient);

            hostClient.Open();

            Console.WriteLine("Servis je pokrenut.");

            NetTcpBinding bindingBackup = new NetTcpBinding();
            string addressBackup = "net.tcp://localhost:4002/Backup";

            //Certificate Authentication
            bindingBackup.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            ServiceHost hostBackup = new ServiceHost(typeof(DataBaseService));
            hostBackup.AddServiceEndpoint(typeof(IBackupService), bindingBackup, addressBackup);

            hostBackup.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
            hostBackup.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            hostBackup.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, name);

            hostBackup.Open();

            Console.WriteLine("BackupService endpoint is opened.");

            Console.ReadLine();

            hostBackup.Close();

            hostClient.Close();
        }
    }
}

