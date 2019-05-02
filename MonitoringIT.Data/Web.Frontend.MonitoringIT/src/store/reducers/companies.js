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
            console.log("action", action)
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
        default:
            return state;
    }
}