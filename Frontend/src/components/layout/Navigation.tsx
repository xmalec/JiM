import { useNavigate } from "react-router-dom";
import useLoggedInUser from "../../hooks/useLoggedInUser";

const Navigation = () => {
	const navigate = useNavigate();
	const [user, auth] = useLoggedInUser();
	return <div>Navigation bar</div>;
};

export default Navigation;
