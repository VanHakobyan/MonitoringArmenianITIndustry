import types from "store/types";

// by page
export const requestCompanies = count => ({ type: types.REQUESTED_COMPANIES, count });
export const succeededCompanies = profiles => ({ type: types.SUCCEEDED_COMPANIES, profiles });
export const failedCompanies = error => ({ type: types.FAILED_COMPANIES, error });

// presentation data
export const requestFavoriteCompanies = count => ({ type: types.REQUESTED_FAVORITE_COMPANIES, count });
export const succeededFavoriteCompanies = profiles => ({ type: types.SUCCEEDED_FAVORITE_COMPANIES, profiles });
export const failedFavoriteCompanies = error => ({ type: types.FAILED_FAVORITE_COMPANIES, error });

//
export const setCurrentPage = page => ({ type: types.SET_CURRENT_COMPANIES_PAGE, page });