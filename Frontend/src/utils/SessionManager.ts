import { User } from "../hooks/useLoggedInUser";

const SessionManager = {
	getToken: () => {
		const token = sessionStorage.getItem("token");
		if (token) return token;
		else return null;
	},

	getUser: (): User | undefined => {
		const token = sessionStorage.getItem("token");
		const email = sessionStorage.getItem("email");
		const id = sessionStorage.getItem("id");
		const isAdmin =
			sessionStorage.getItem("isAdmin")?.toLocaleLowerCase() === "true";
		const isCompanyOwner = !!sessionStorage.getItem("isCompanyOwner");
		const companyId = sessionStorage.getItem("companyId");
		const isApproved =
			sessionStorage.getItem("isApproved")?.toLocaleLowerCase() ===
			"true";
		if (!token || !email || !id) {
			return undefined;
		}
		return {
			token,
			email,
			id,
			companyId: companyId ?? "0",
			isCompanyOwner,
			isAdmin,
			isApproved,
		};
	},

	setUserSession: (user: User) => {
		sessionStorage.setItem("token", user.token);
		sessionStorage.setItem("id", user.id);
		sessionStorage.setItem("email", user.email);
		sessionStorage.setItem("isAdmin", user.isAdmin.toString());
		sessionStorage.setItem("isApproved", user.isApproved?.toString() ?? "");
		sessionStorage.setItem(
			"isCompanyOwner",
			user.isCompanyOwner?.toString() ?? "0"
		);
		sessionStorage.setItem("companyId", user.companyId?.toString() ?? "0");
	},

	removeUserSession: () => {
		sessionStorage.removeItem("token");
		sessionStorage.removeItem("id");
		sessionStorage.removeItem("email");
		sessionStorage.removeItem("isAdmin");
		sessionStorage.removeItem("isCompanyOwner");
		sessionStorage.removeItem("companyId");
		sessionStorage.removeItem("isApproved");
	},
};

export default SessionManager;
