using Carpo.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace ShoppingTest.Campaign.Core.Domain
{
    public class CampaignDomain : BaseDomain
    {
        /// <summary>
        /// Identity
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Campaign name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Initial date
        /// </summary>
        public DateTime InitialDate { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Identity
        /// </summary>
        [Required]
        public string IdShopping { get; set; }

    }
}
