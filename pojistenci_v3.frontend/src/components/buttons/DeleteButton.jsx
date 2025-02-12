import React from "react";

export function DeleteButton({ confirmDelete, setConfirmDelete, handleDelete }) {
  return (
    <div className="position-relative">
      {confirmDelete && (
        <div className="confirm-delete-overlay position-absolute w-100">
          <p className="text-center text-danger fw-bold">Opravdu smazat?</p>
          <button
            onClick={handleDelete}
            className="btn btn-danger rounded-0 w-100"
          >
            Ano, smazat
          </button>
          <button
            onClick={() => setConfirmDelete(false)}
            className="btn btn-secondary rounded-0 w-100"
          >
            Zrušit
          </button>
        </div>
      )}
      <button
        onClick={() => setConfirmDelete(true)}
        className="btn btn-danger rounded-0 w-100"
        style={{ visibility: confirmDelete ? "hidden" : "visible" }}
      >
        Smazat
      </button>
    </div>
  );
}
