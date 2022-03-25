using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Infrastructure.Configuration
{
    public interface IApplicationSettings
    {
        string BaseStorageAccount { get; }
        string StorageAccountKeyPrefix { get; }
    }

    public class ApplicationSettings : IApplicationSettings
    {
        public string BaseStorageAccount { get; set; } = string.Empty;

        public string StorageAccountKeyPrefix { get; set; } = string.Empty;
    }

    public class ApplicationSettingsAdapter : IApplicationSettings
    {
        private readonly IOptions<ApplicationSettings> _options;

        public ApplicationSettingsAdapter(IOptions<ApplicationSettings> options)
        {
            _options = options;
        }

        public string BaseStorageAccount => _options.Value.BaseStorageAccount;

        public string StorageAccountKeyPrefix => _options.Value.StorageAccountKeyPrefix;
    }
}
