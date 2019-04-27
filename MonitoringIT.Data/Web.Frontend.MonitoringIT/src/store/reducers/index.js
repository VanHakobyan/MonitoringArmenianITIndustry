import { combineReducers } from "redux";
import githubProfiles from "store/reducers/githubProfiles";

const rootReducer = combineReducers({
	githubProfiles
});

export default rootReducer;