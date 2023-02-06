import { Routes, Route } from "react-router-dom";

import useLoggedInUser from "../../hooks/useLoggedInUser";
import Login from "../../pages/Login";
import NotFound from "../../pages/NotFound";

const Routing = () => {
	const [user] = useLoggedInUser();
	return (
		<Routes>
			{!user && (
				<>
					<Route path="/login" element={<Login />} />
				</>
			)}
			{!!user?.isAdmin === true && (
				<>
					{/* <Route path="/companies" element={<Companies />} />
					<Route path="/companies/:id" element={<Company />} /> */}
				</>
			)}
			{!!user?.isAdmin === false && (
				<>
					{/* <Route path="/cart" element={<Cart />} />
					<Route path="/company" element={<Company />} />
					<Route path="/" element={<Products />} /> */}
				</>
			)}
			<Route path="*" element={<NotFound />} />
		</Routes>
	);
};

export default Routing;
