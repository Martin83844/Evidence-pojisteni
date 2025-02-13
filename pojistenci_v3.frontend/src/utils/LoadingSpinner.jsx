export function LoadingSpinner() {
  return (
    <div
      className="d-flex justify-content-center align-items-center"
      style={{
        height: "100vh",
        width: "100%",
        position: "fixed",
        top: 0,
        left: 0,
        backgroundColor: "rgba(255, 255, 255, 0.8)",
        zIndex: 9999
      }}
    >
      <div className="spinner-border text-primary" role="status">
        <span className="visually-hidden">Načítání...</span>
      </div>
    </div>
  )
}