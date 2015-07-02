#if run_with_bin_sh 
  # See why this works at http://stackoverflow.com/a/21948918/637783 
  exec fsharpi --define:mono_posix --exec $0 $*
#endif
