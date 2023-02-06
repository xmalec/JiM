import {
	createContext,
	Dispatch,
	FC,
	PropsWithChildren,
	SetStateAction,
	useContext,
	useEffect,
	useState,
} from "react";

import SessionManager from "../utils/SessionManager";

export type User = {
	id: string;
	token: string;
	email: string;
	isAdmin: boolean;
	companyId?: string;
	isCompanyOwner?: boolean;
	isApproved?: boolean;
};

type UserState = [User | undefined, Dispatch<SetStateAction<User | undefined>>];
const UserContext = createContext<UserState>(undefined as never);

export const UserProvider: FC<PropsWithChildren> = ({ children }) => {
	const userState = useState(SessionManager.getUser());
	useEffect(() => {
		const updateState = () => userState[1](SessionManager.getUser());
		addEventListener("storage", updateState);
		return () => {
			removeEventListener("storage", updateState);
		};
	}, []);
	return (
		<UserContext.Provider value={userState}>
			{children}
		</UserContext.Provider>
	);
};

const useLoggedInUser = () => {
	const user = useContext(UserContext)[0];
	return [
		user,
		(newVal: User | null) => {
			if (newVal !== null) {
				SessionManager.setUserSession(newVal);
			} else {
				SessionManager.removeUserSession();
			}
			dispatchEvent(new Event("storage"));
		},
	] as const;
};

export default useLoggedInUser;
