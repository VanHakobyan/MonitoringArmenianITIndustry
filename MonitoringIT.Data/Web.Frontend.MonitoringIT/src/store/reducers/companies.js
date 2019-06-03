import types from "store/types";

export default function reduce(state = {}, action) {
	switch (action.type) {
		case types.REQUESTED_FAVORITE_COMPANIES:
			return {
				...state,
				favoriteCompaniesLoading: true,
				favoriteCompaniesSuccess: undefined,
				favoriteCompaniesFailed: undefined
			};
		case types.SUCCEEDED_FAVORITE_COMPANIES:
			return {
				...state,
				favoriteCompaniesLoading: true,
				favoriteCompaniesSuccess: action.profiles,
				favoriteCompaniesFailed: undefined
			};
		case types.FAILED_FAVORITE_COMPANIES:
			return {
				...state,
				favoriteCompaniesLoading: true,
				favoriteCompaniesSuccess: undefined,
				favoriteCompaniesFailed: action.error
			};
		case types.REQUESTED_COMPANIES:
			return {
				...state,
				companiesLoading: true,
				companiesFailed: undefined
			};
		case types.SUCCEEDED_COMPANIES:
			return {
				...state,
				companiesLoading: true,
				companiesSuccess: action.profiles,
				companiesFailed: undefined
			};
		case types.FAILED_COMPANIES:
			return {
				...state,
				companiesLoading: true,
				companiesSuccess: undefined,
				companiesFailed: action.error
			};
		case types.SET_CURRENT_COMPANIES_PAGE:
			return {
				...state,
				currentCompaniesPage: action.page
			};
		default:
			return state;
	}
}