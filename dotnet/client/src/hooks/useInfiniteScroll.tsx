import React, { useEffect } from "react";

const useInfiniteScroll = (nextCursor: string) => {
  const [cursor, setCursor] = React.useState<string>('0');
  const ref = React.useRef(null);

  const handleObserver = React.useCallback((entries: any) => {
    const [target] = entries;

    if (target.isIntersecting) {
      setCursor(nextCursor);
    }
  }, []);

  useEffect(() => {
    const option = {
      root: null,
      rootMargin: '0px',
      threshold: 1.0
    }

    const observer = new IntersectionObserver(handleObserver, option);

    if (ref.current) { observer.observe(ref.current); }

  }, [handleObserver])

  return { cursor, ref };

}

export default useInfiniteScroll;
