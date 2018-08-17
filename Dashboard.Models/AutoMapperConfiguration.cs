using System;
using AutoMapper;
using Dashboard.Models.BorrowingProfile;

namespace Dashboard.Models
{
    public class AutoMapperConfiguration
    {
        private static readonly object _thisLock = new object();
        private static bool _initialized;
        // Centralize automapper initialize
        public static void Initialize()
        {
            // This will ensure one thread can access to this static initialize call
            // and ensure the mapper is reseted before initialized
            lock (_thisLock)
            {
                if (_initialized) return;
                Mapper.Initialize(ConfigAction);
                _initialized = true;
            }
        }
        public static readonly Action<IMapperConfigurationExpression> ConfigAction = x =>
        {
            x.AddProfile<BorrowingProfileMappingProfile>();
        };
    }
}