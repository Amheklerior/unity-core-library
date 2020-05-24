using System;

namespace Amheklerior.Core.ObjectPooling {

    public interface IPool<T> {
        Action<T> OnGet { get; set; }
        Action<T> OnPut { get; set; }
        T Get();
        void Put(T instance);
    }

}