import types from "store/types";
// presentation data
export const requestFavoriteCompanies = count => ({ type: types.REQUESTED_FAVORITE_COMPANIES, count });
export const succeededFavoriteCompanies = profiles => ({ type: types.SUCCEEDED_FAVORITE_COMPANIES, profiles });
export const failedFavoriteCompanies = error => ({ type: types.FAILED_FAVORITE_COMPANIES, error });


// by page
export const requestCompaniesByPage = (currentPage, count) =>
    ({ type: types.REQUESTED_COMPANIES_BY_PAGE, currentPage, count });
export const succeededCompaniesByPage = profiles => ({ type: types.SUCCEEDED_COMPANIES_BY_PAGE, profiles });
export const failedCompaniesByPage = error => ({ type: types.FAILED_COMPANIES_BY_PAGE, error });

