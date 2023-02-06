import "./App.css";
import "./output.css";
import { UserProvider } from "./hooks/useLoggedInUser";
import { BrowserRouter } from "react-router-dom";
import Layout from "./components/layout/Layout";

const App = () => (
	<UserProvider>
		<BrowserRouter>
			<Layout />
		</BrowserRouter>
	</UserProvider>
);

export default App;
