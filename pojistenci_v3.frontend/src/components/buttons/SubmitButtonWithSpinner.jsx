import React from "react";

export function SubmitButtonWithSpinner({ submitting, buttonText = "Uložit změny", loadingText = "Ukládání...", height = "40px" }) {
  return (
    <div className="card-footer row m-0 p-0" style={{ height }}>
      {submitting ? (
        <button className="btn btn-secondary rounded-0 w-100 d-flex justify-content-center align-items-center" type="button" disabled>
          {loadingText}
          <div className="spinner-border spinner-border-sm ms-2" role="status">
            <span className="visually-hidden">{loadingText}</span>
          </div>
        </button>
      ) : (
        <button className="btn btn-secondary rounded-0 w-100" type="submit">
          {buttonText}
        </button>
      )}
    </div>
  );
}
