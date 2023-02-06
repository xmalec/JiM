import LoginForm from "../forms/LoginForm";
import usePageTitle from "../hooks/usePageTitle";

const Login = () => {
	usePageTitle("Login");
	return <LoginForm></LoginForm>;
};

export default Login;
