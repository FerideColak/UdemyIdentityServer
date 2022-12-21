using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace UdemyIdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1"){
                    Scopes={"api1.read", "api1.write", "api1.update"},
                    ApiSecrets = new[] { new Secret("secretapi1".Sha256()) } },
                new ApiResource("resource_api2"){
                    Scopes={"api2.read", "api2.write", "api2.update"},
                    ApiSecrets = new[] { new Secret("secretapi2".Sha256()) } }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("api1.read", "API 1 için okuma izni"),
                new ApiScope("api1.write", "API 1 için yazma izni"),
                new ApiScope("api1.update", "API 1 için güncelleme izni"),
                new ApiScope("api2.read", "API 2 için okuma izni"),
                new ApiScope("api2.write", "API 2 için yazma izni"),
                new ApiScope("api2.update", "API 2 için güncelleme izni")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(), //subId
                /*
                    Bir token dondüğü zaman içerisinde mutlaka bir kullanıcı id'si bulunmak zorundadır ve bu "Subject Id" olarak geçer.
                    Bu token, bu id'ye sahip kullanıcı hakkında demektir. Token'ın kim için üretildiği belirtilmiş olur.
                 */
                new IdentityResources.Profile(),  //profile içerisinde de claimler tutulur, default claimler bulunduğu gibi ozel olarak da tanımlanabilir.
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser() { SubjectId = "1", Username = "fcolak", Password = "123456", Claims = new List<Claim>(){
                    new Claim("given_name", "feride"),
                    new Claim("family_name", "colak")
                } },
                new TestUser() { SubjectId = "2", Username = "aycatrkm", Password = "567890", Claims=new List<Claim>(){
                    new Claim("given_name", "ayca"),
                    new Claim("family_name", "turkmen")
                } }
                // eğer kullanıcı adı yerine email ile giriş yapılması istenirse username alanında email tutulabilir 
                // claimler, token içerisinde bulunacak datalar
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "Client1",
                    ClientName = "Client 1 app uygulaması",
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    AllowedGrantTypes =  GrantTypes.ClientCredentials,
                    AllowedScopes = {"api1.read"}
                },

                new Client()
                {
                    ClientId = "Client2",
                    ClientName = "Client 2 app uygulaması",
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    AllowedGrantTypes =  GrantTypes.ClientCredentials,
                    AllowedScopes = {"api1.read", "api1.update", "api2.write", "api2.update"}
                }
            };
        }

    }
}
