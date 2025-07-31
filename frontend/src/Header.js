export default function Header() {
  return (
    <section className="block hero is-link">
      <div className="hero-body">
        <p className="title">
          <a className="has-text-inherit" href="/">
            <span>TaskMan</span>
          </a>
          <span className="is-size-7 ml-2" title="More info">0.1</span>
        </p>
        <p className="subtitle is-size-6">A simple task manager</p>
      </div>
    </section>
  );
}
