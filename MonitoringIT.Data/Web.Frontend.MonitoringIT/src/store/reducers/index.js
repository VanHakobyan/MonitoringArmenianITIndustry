import { combineReducers } from "redux";
import githubProfiles from "store/reducers/githubProfiles";
import linkedinProfiles from "store/reducers/linkedinProfiles";
import companies from "store/reducers/companies";

const rootReducer = combineReducers({
	githubProfiles,
	companies,
	linkedinProfiles

});

export default rootReducer;