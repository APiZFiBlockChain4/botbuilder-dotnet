﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Bot.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Bot.Configuration.Encryption;
    using Newtonsoft.Json;

    public class GenericService : ConnectedService
    {
        public GenericService()
            : base(ServiceTypes.Generic)
        {
        }

        /// <summary>
        /// Gets or sets url for deep link to service.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets configuration.
        /// </summary>
        [JsonProperty("configuration")]
        public Dictionary<string, string> Configuration { get; set; } = new Dictionary<string, string>();

        /// <inheritdoc/>
        public override void Encrypt(string secret)
        {
            base.Encrypt(secret);

            foreach (var key in this.Configuration.Keys.ToArray())
            {
                this.Configuration[key] = this.Configuration[key].Encrypt(secret);
            }
        }

        /// <inheritdoc/>
        public override void Decrypt(string secret)
        {
            base.Decrypt(secret);

            foreach (var key in this.Configuration.Keys.ToArray())
            {
                this.Configuration[key] = this.Configuration[key].Decrypt(secret);
            }
        }
    }
}
