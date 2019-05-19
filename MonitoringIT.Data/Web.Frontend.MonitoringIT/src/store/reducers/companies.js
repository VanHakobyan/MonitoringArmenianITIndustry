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


        case types.REQUESTED_COMPANIES_BY_PAGE:
            return {
                ...state,
                byPageCompaniesLoading: true,
                byPageCompaniesSuccess: undefined,
                byPageCompaniesFailed: undefined
            };
        case types.SUCCEEDED_COMPANIES_BY_PAGE:
            return {
                ...state,
                byPageCompaniesLoading: true,
                byPageCompaniesSuccess: action.profiles,
                byPageCompaniesFailed: undefined
            };
        case types.FAILED_COMPANIES_BY_PAGE:
            return {
                ...state,
                byPageCompaniesLoading: true,
                byPageCompaniesSuccess: undefined,
                byPageCompaniesFailed: action.error
            };
        default:
            return state;
    }
}