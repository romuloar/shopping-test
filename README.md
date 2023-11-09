# shopping-test
This project deals with a marketing system for shopping centers in .net core

<h3>Architecture Diagram</h3>
<img src='https://github.com/romuloar/shopping-test/assets/9424233/69700e23-996c-4e3e-ab0e-8b777266837c' />

<h3>API Gateway</h3>
<ul>
  <li>Ocelot</li>
</ul>

<h3>API Infra</h3>
<ul>
  <li>
      Auth API(Authentication and Authorization)
      <h4>Snippet code example using Identity</h4>
    <pre>
      <code>
      using Microsoft.AspNetCore.Identity;
      using Microsoft.Extensions.Configuration;
      using Microsoft.IdentityModel.Tokens;
      using System;
      using System.IdentityModel.Tokens.Jwt;
      using System.Security.Claims;
      using System.Text;
      using System.Threading.Tasks;
    
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        public TokenService(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> GenerateTokenAsync(string username)
        {
            // Get the user by username
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                // Handle the case when the user doesn't exist
                throw new ApplicationException("User not found.");
            }
            // Create claims with user information
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
  </code>
  </pre> <br />
  </li>
  <li>Caching API</li>
  <li>Other API</li>
</ul>

<h3>API Services using clean architecture</h3>
<ul>
  <li>Campaign API - For now the repository project is using Sqlite with migrations to start the project in a second moment it is possible to use SQL server or another database</li>
  <li>Notification API</li>
  <li>Offer API</li>
</ul>

<h3>Redis snippet code for campaign</h3>
<pre>
  <code>
  using StackExchange.Redis;
  using System;  
  class Program
  { 
     static void Main(string[] args)
      {
          // Redis server connection configuration
          string redisHost = "localhost"; // Redis server address
          int redisPort = 6379; // Default Redis port
          ConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{redisHost}:{redisPort}");
          IDatabase database = redis.GetDatabase();
          // Marketing campaign information
          string campaignName = "BlackFriday2023";
          DateTime startDate = new DateTime(2023, 11, 24); // Campaign start date
          DateTime endDate = new DateTime(2023, 11, 27); // Campaign end date
          // Store campaign information in Redis
          var campaignDetails = new HashEntry[]
          {
              new HashEntry("Name", campaignName),
              new HashEntry("StartDate", startDate.ToString("yyyy-MM-dd")),
              new HashEntry("EndDate", endDate.ToString("yyyy-MM-dd"))
          };
          string campaignKey = "Campaign:" + campaignName;
          database.HashSet(campaignKey, campaignDetails);
          Console.WriteLine($"Campaign '{campaignName}' added to Redis.");
          // Example of how to retrieve campaign details
          var storedDetails = database.HashGetAll(campaignKey);
          Console.WriteLine($"Campaign '{campaignName}' details:");
          foreach (var entry in storedDetails)
          {
              Console.WriteLine($"{entry.Name}: {entry.Value}");
          }
          redis.Close();
      }
  }
  </code>
</pre>
<h3>Event Bus (RabbitMQ)</h3>
<pre>
<code>
  using System;
  using RabbitMQ.Client;
  using System.Text;
  class Program
  {
        static void Main(string[] args)
        {
             // RabbitMQ connection configuration
             var factory = new ConnectionFactory()
             {
                HostName = "localhost", // RabbitMQ server address
                Port = 5672,            // RabbitMQ default port
                UserName = "guest",     // Username
                Password = "guest"      // Password
             };
             using (var connection = factory.CreateConnection())
             using (var channel = connection.CreateModel())
             {
                // Name of the queue for sending user notifications
                string queueName = "userNotifications";
                // Declare the queue (create it if it doesn't exist)
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                // Message content for the notification
                string notificationMessage = "Special sale event at our shopping mall! Visit us today.";
                // Convert the notification message to bytes
                byte[] body = Encoding.UTF8.GetBytes(notificationMessage);
                // Publish the notification message to the queue
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                Console.WriteLine("Notification sent: {0}", notificationMessage);
             }
             Console.WriteLine("Press any key to exit.");
             Console.ReadKey();
         }
  }  </code>
</pre>
In this example, the code sends a generic notification to the “userNotifications” queue in RabbitMQ. You need to customize the code to include specific information about the mall, such as notification type, target audience, and other relevant details. Additionally, I must create systems that consume queues to deliver notifications to users.

<hr />

<h3>GDPR sample code for data protection</h3>
<pre>
<code>
  using System;
  using System.Security.Cryptography;

  public class DataProtectionExample
  {    
    
    public static string EncryptData(string data, string encryptionKey)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Convert.FromBase64String(encryptionKey);
            aesAlg.GenerateIV();
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] encryptedData;
            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(data);
                    }
                }
                encryptedData = msEncrypt.ToArray();
            }
            byte[] combinedData = new byte[aesAlg.IV.Length + encryptedData.Length];
            Array.Copy(aesAlg.IV, 0, combinedData, 0, aesAlg.IV.Length);
            Array.Copy(encryptedData, 0, combinedData, aesAlg.IV.Length, encryptedData.Length);
            return Convert.ToBase64String(combinedData);
        }
    }
    public static string DecryptData(string encryptedData, string encryptionKey)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Convert.FromBase64String(encryptionKey);
            byte[] combinedData = Convert.FromBase64String(encryptedData);
            byte[] iv = new byte[aesAlg.IV.Length];
            byte[] cipherText = new byte[combinedData.Length - aesAlg.IV.Length];
            Array.Copy(combinedData, 0, iv, 0, iv.Length);
            Array.Copy(combinedData, iv.Length, cipherText, 0, cipherText.Length);
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);
            using (var msDecrypt = new System.IO.MemoryStream(cipherText))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
</code>
</pre>

