# Playing around with await and ConfigureAwait

* await doesn't change thread unless we hit some system call, that means that nestet await calls do not enter another thread unless the last await call

* await, if ConfigureAwait(false) isn't specified, always tries to return SynchronizationContext to what it was before the call

* if we have awat w\o ConfigureAwait(false) and down the call chain we have one with ConfigureAwait(false) we could end up in the same thread after all, but after passing the latest await we won't have a synchronization context, and after passing the first one it will be restored. Everything on the same thread
