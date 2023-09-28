import { useEffect } from 'react'

const LoginWithGoogle = (props) => {

  useEffect(() => {

    window.onGoogleSuccess = (response) => {
      props.onSuccessfulLogin(response);
    }

    // Inject the google provided script 
    // (an importable module would be nicer here)
    const script = document.createElement('script');
    script.src = "https://accounts.google.com/gsi/client";
    script.async = true;
    document.body.appendChild(script);

    return () => {
      // clean up for react lifecycle
      window.onGoogleSuccess = undefined;
      document.body.removeChild(script)
    }
  }, []);

  return (
    <div>
        <div id="g_id_onload"
            data-client_id="291479196641-kd7mr5hkaiv03i4qjpltot5d8so3do5q.apps.googleusercontent.com"
            data-context="signin"
            data-ux_mode="popup"
            data-callback="onGoogleSuccess"
            data-nonce=""
            data-auto_prompt="false">
        </div>

        <div class="g_id_signin"
            data-type="standard"
            data-shape="rectangular"
            data-theme="filled_blue"
            data-text="signin_with"
            data-size="large"
            data-logo_alignment="left">
        </div>
    </div>
  )
}

export default LoginWithGoogle;